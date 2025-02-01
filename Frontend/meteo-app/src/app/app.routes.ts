import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import {LocationWeatherComponent} from './location-weather/location-weather.component';
import {RouterModule, Routes} from '@angular/router';
import {LoginComponent} from './login/login.component';
import {RegisterComponent} from './register/register.component';
import { AuthGuard } from './auth.guard';

export const routes: Routes = [
  { path: 'login', component: LoginComponent },
  { path: 'register', component: RegisterComponent },
  { path: 'weather', component: LocationWeatherComponent, canActivate: [AuthGuard]},
  { path: '', redirectTo: '/weather', pathMatch: 'full'},
  { path: '**', redirectTo: '/' }]

@NgModule({
  declarations: [],
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
