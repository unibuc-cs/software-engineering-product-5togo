import { Component, NgModule, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';

@Component({
  selector: 'app-location-weather',
  imports: [FormsModule, CommonModule, HttpClientModule],
  templateUrl: './location-weather.component.html',
  styleUrl: './location-weather.component.css'
})

export class LocationWeatherComponent {
  isNavActive = false;
  weatherData: any = null;
  searchQuery = '';
  bookmarkedLocations:Set<string> = new Set();
  next5DaysWeather: any = null;
  forecasts: any = null;
  userLatitude: number = 0;
  userLongitude: number = 0;

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {
  }

  ngOnInit() {
    this.loadBookmarkedLocations();
    this.loadClosestLocation();
    this.loadWeatherForecasts();


    if (this.next5DaysWeather?.forecasts?.$values?.length) {
      this.forecasts = this.next5DaysWeather.forecasts.$values;
      console.log("Resulted forecast" + this.forecasts);
    }
    this.searchQuery = '';
  }

  toggleNav() {
    this.isNavActive = !this.isNavActive;
  }

  searchLocation() {
    console.log('Searching for:', this.searchQuery);

    if (!this.searchQuery) return;

    this.http
      .get(`http://localhost:5114/api/Weather/location/${this.searchQuery}`)
      .subscribe(
        (data: any) => {
          this.weatherData = data; // Update weather data
          console.log('Weather data received:', this.weatherData);
        },
        (error) => {
          console.error('Error fetching weather data:', error);
          alert('Location not found or an error occurred.');
        }
      );

  }

  favoriteSearch(location: any) {
    this.searchQuery = location;
    this.searchLocation();
    this.searchQuery = '';
  }

  addToFavorites() {
    if (!this.weatherData?.locationName) return;

    const locationName = this.weatherData.locationName;
    const token = localStorage.getItem('token');

    if (!token) {
      alert("You are not authenticated!");
      return;
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    this.http
      .post('http://localhost:5100/api/FavoriteLocations/add-to-favourites', { locationName }, { headers })
      .subscribe(
        (response: any) => {
          alert("Adaugat la favorite!");
          this.loadBookmarkedLocations();
          console.log('Added to favourites!');
        },
        (error) => {
          console.error('Error adding to favorites:', error);
          alert('Failed to add location to favorites.');
        }
      );
  }

  loadBookmarkedLocations() {
    const token = localStorage.getItem('token');

    if (!token) {
      alert("You are not authenticated!");
      return;
    }

    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`,
      'Content-Type': 'application/json'
    });

    this.http
      .get('http://localhost:5100/api/FavoriteLocations/get-favourites', { headers })
      .subscribe(
        (data: any) => {
          this.bookmarkedLocations = new Set(data);
        },
        (error) => {
          console.error('Error fetching bookmarked locations:', error);
        }
      );
  }

  loadWeatherForecasts() {
    this.http
      .get(`http://localhost:5114/api/Weather/forecast/${this.weatherData.locationName}`)
      .subscribe(
        (data: any) => {
          this.next5DaysWeather = data;
          console.log('Forecast data received:', this.next5DaysWeather);
        },
        (error) => {
          console.error('Error fetching weather data:', error);
        }
      );
  }

  getUserLocation(){
    if (navigator.geolocation) {
      navigator.geolocation.getCurrentPosition(
        (position) => {
          this.userLatitude = position.coords.latitude;
          this.userLongitude = position.coords.longitude;
          console.log(`Latitude: ${this.userLatitude}, Longitude: ${this.userLongitude}`);
        },
        (error) => {
          console.error('Error getting location:', error);
          switch (error.code) {
            case error.PERMISSION_DENIED:
              console.log('User denied the request for Geolocation.');
              break;
            case error.POSITION_UNAVAILABLE:
              console.log('Location information is unavailable.');
              break;
            case error.TIMEOUT:
              console.log('The request to get user location timed out.');
              break;
          }
        }
      );
    } else {
      console.log('Geolocation is not supported by this browser.');
    }
  }

  loadClosestLocation(){
    this.getUserLocation();

    const params = {
      coordinates: [this.userLatitude.toString(), this.userLongitude.toString()],
    };

    this.http
      .get(`http://localhost:5114/api/Weather/location/current-location`, { params })
      .subscribe(
        (data: any) => {
          this.weatherData = data;
          console.log('Weather data received:', this.weatherData);
        },
        (error) => {
          console.error('Error fetching weather data:', error);
        }
      );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

  get bookmarkedLocationsArray() {
    return Array.from(this.bookmarkedLocations);
  }

  protected readonly console = console;
}

