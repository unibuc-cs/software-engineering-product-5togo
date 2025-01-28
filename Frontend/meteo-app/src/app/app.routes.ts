import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LocationWeatherComponent} from './location-weather/location-weather.component';
import {RouterModule, Routes} from '@angular/router';

export const routes: Routes = [
  { path: 'weather', component: LocationWeatherComponent },
  { path: '', redirectTo: '/weather', pathMatch: 'full' }]

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
