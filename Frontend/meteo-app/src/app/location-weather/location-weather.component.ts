import { Component, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';
import { HttpClientModule } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

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
  bookmarkedLocations = [];

  constructor(private http: HttpClient, private authService: AuthService, private router: Router) {
    this.searchQuery = "ADAMCLISI";
    this.searchLocation();
  }

  toggleNav() {
    this.isNavActive = !this.isNavActive;
  }

  searchLocation() {
    console.log('Searching for:', this.searchQuery);

    if (!this.searchQuery) return;

    this.http
      .get(`http://localhost:5114/Weather/location/${this.searchQuery}`)
      .subscribe(
        (data: any) => {
          this.weatherData = data; // Update weather data
        },
        (error) => {
          console.error('Error fetching weather data:', error);
          alert('Location not found or an error occurred.');
        }
      );

  }

  addToFavorites() {
    if (!this.weatherData?.nume) return;

    const locationName = this.weatherData.nume;

    this.http
      .post('http://localhost:5100/FavoriteLocation', { locationName })
      .subscribe(
        (response: any) => {
          alert(response.Message);
          this.loadBookmarkedLocations();
        },
        (error) => {
          console.error('Error adding to favorites:', error);
          alert('Failed to add location to favorites.');
        }
      );
  }

  loadBookmarkedLocations() {
    this.http
      .get('http://localhost:5100/FavoriteLocations')
      .subscribe(
        (data: any) => {
          this.bookmarkedLocations = data;
        },
        (error) => {
          console.error('Error fetching bookmarked locations:', error);
        }
      );
  }

  logout() {
    this.authService.logout();
    this.router.navigate(['/login']);
  }

}

