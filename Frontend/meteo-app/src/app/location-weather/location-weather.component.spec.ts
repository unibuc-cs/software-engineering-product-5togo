import { ComponentFixture, TestBed } from '@angular/core/testing';

import { LocationWeatherComponent } from './location-weather.component';

describe('LocationWeatherComponent', () => {
  let component: LocationWeatherComponent;
  let fixture: ComponentFixture<LocationWeatherComponent>;

  beforeEach(async () => {
    await TestBed.configureTestingModule({
      imports: [LocationWeatherComponent]
    })
    .compileComponents();

    fixture = TestBed.createComponent(LocationWeatherComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
