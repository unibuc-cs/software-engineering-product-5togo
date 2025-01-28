import { Component } from '@angular/core';

@Component({
  selector: 'app-location-weather',
  imports: [],
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
    icon: "2",
    nume: "ADAMCLISI",
    tempe: "15.5",
    vant: "3.0 m/s, directia : S",
    tempapa: "indisponibil"
  };

  toggleNav() {
    this.isNavActive = !this.isNavActive;
  }
}

