import { Component, NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-location-weather',
  imports: [FormsModule, CommonModule],
  templateUrl: './location-weather.component.html',
  styleUrl: './location-weather.component.css'
})

export class LocationWeatherComponent {
  isNavActive = false;
  weatherData = {
    umezeala: 56,
    fenomen_e: "indisponibil",
    zapada: "indisponibil",
    actualizat: "28-01-2025 ora 14:00",
    nebulozitate: "cer partial noros",
    presiunetext: "995.3 mb, in scadere",
    icon: "51",
    nume: "ADAMCLISI",
    tempe: "15.5",
    vant: "3.0 m/s, directia : S",
    tempapa: "indisponibil"
  };
  searchQuery = '';
  bookmarkedLocations = [
    { name: 'Bucharest', temperature: 18 },
    { name: 'Cluj-Napoca', temperature: 15 },
  ];
  userName = 'John Doe';

  toggleNav() {
    this.isNavActive = !this.isNavActive;
  }

  searchLocation() {
    console.log('Searching for:', this.searchQuery);
  }

  logout() {
    console.log('User logged out');
  }

}

