import { Component, NgModule, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';
import { HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs/internal/Observable';

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
  
    this.loadClosestLocation().subscribe((data: any) => {
      this.weatherData = data;
      console.log('Closest location received:', this.weatherData.locationName);
  
      var location;
      if (this.weatherData.locationName.toUpperCase().includes("BUCURESTI")) {
        location = "Bucuresti";
      } else {
        location = this.weatherData.locationName;
      }
  
      console.log('Fetching forecast for:', location);
  
      this.loadWeatherForecasts(location).subscribe(
        (forecastData: any) => {
          this.next5DaysWeather = forecastData;
          console.log('Forecast data received:', this.next5DaysWeather);
  
          if (this.next5DaysWeather?.forecasts?.$values?.length) {
            this.forecasts = this.next5DaysWeather.forecasts.$values;
          } else {
            console.log('No forecasts found.');
            this.forecasts = null;
          }
  
          this.searchQuery = '';
        },
        (error) => {
          console.error('Error fetching weather data:', error);
        }
      );
    });
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
          this.weatherData = data;
          console.log('Weather data received:', this.weatherData);
  
          let location = this.weatherData.locationName;
          if (location.toUpperCase().includes("BUCURESTI")) {
            location = "Bucuresti";
          }
  
          console.log('Fetching forecast for:', location);
  
          this.loadWeatherForecasts(location).subscribe(
            (forecastData: any) => {
              this.next5DaysWeather = forecastData;
              console.log('Forecast data received:', this.next5DaysWeather);
  
              if (this.next5DaysWeather?.forecasts?.$values?.length) {
                this.forecasts = this.next5DaysWeather.forecasts.$values;
              } else {
                console.log('No forecasts found.');
                this.forecasts = null;
              }
  
              this.searchQuery = '';
            },
            (error) => {
              console.error('Error fetching weather data:', error);
            }
          );
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
  
    this.loadWeatherForecasts(location).subscribe(
      (data: any) => {
        this.next5DaysWeather = data;
        console.log('Forecast data received:', this.next5DaysWeather);
  
        if (this.next5DaysWeather?.forecasts?.$values?.length) {
          this.forecasts = this.next5DaysWeather.forecasts.$values;
        } else {
          console.log(this.next5DaysWeather?.forecasts?.$values?.length);
          this.forecasts = null;
        }
  
        this.searchQuery = '';
      },
      (error) => {
        console.error('Error fetching weather data:', error);
      }
    );
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

  loadWeatherForecasts(location: string): Observable<any> {
    return this.http.get(`http://localhost:5114/api/Weather/forecast/${location}`);
  }
  

  loadClosestLocation(): Observable<any> {
    return new Observable((observer) => {
      if (navigator.geolocation) {
        navigator.geolocation.getCurrentPosition(
          (position) => {
            this.userLatitude = position.coords.latitude;
            this.userLongitude = position.coords.longitude;
            console.log(`Latitude: ${this.userLatitude}, Longitude: ${this.userLongitude}`);
  
            const params = {
              coordinates: [this.userLatitude.toString(), this.userLongitude.toString()],
            };
  
            this.http.get(`http://localhost:5114/api/Weather/location/current-location`, { params })
              .subscribe(
                (data: any) => {
                  this.weatherData = data;
                  observer.next(data); // Trimite datele mai departe
                  observer.complete();
                },
                (error) => {
                  console.error('Error fetching weather data:', error);
                  observer.error(error);
                }
              );
          },
          (error) => {
            console.error('Error getting location:', error);
            observer.error(error);
          }
        );
      } else {
        console.log('Geolocation is not supported by this browser.');
        observer.error('Geolocation not supported.');
      }
    });
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

