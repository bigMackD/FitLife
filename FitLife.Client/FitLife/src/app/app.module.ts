import { BrowserModule } from '@angular/platform-browser';
import { APP_INITIALIZER, NgModule } from '@angular/core';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { RegisterComponent } from './authentication/register/register.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { AngularMaterialModule } from './material.module';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatCardModule, MatExpansionModule, MatMenuModule, MatProgressSpinnerModule, MatSnackBarModule } from '@angular/material';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { LoginComponent } from './authentication/login/login.component';
import { HomeComponent } from './home/home.component';
import { UsersComponent } from './users/users.component';
import { TokenInterceptor } from './authentication/interceptors/token.interceptor';
import { LoaderComponent } from './shared/loader/loader/loader.component';
import { LoaderInterceptor } from './shared/loader/loader.interceptor';
import { UserDialogComponent } from './users/user-dialog/user-dialog.component';
import { NutritionComponent } from './nutrition/nutrition.component';
import { ProductsComponent } from './nutrition/products/products.component';
import { MealsComponent } from './nutrition/meals/meals.component';
import { ProductDetailsComponent } from './nutrition/products/product-details/product-details.component';
import { NgxChartsModule } from '@swimlane/ngx-charts';
import { MealDetailsComponent } from './nutrition/meals/meal-details/meal-details.component';
import { NgSelectModule } from '@ng-select/ng-select';
import {MatSliderModule} from '@angular/material/slider';
import { DashboardComponent } from './dashboard/dashboard.component';
import { RegisterMealDialogComponent } from './dashboard/register-meal-dialog/register-meal-dialog.component';
import { ConfigurationService } from './shared/services/configuration.service';
import { AuthenticationService } from './authentication/services/authentication.service';
import { NotificationService } from './shared/services/notification.service';
import { Router } from '@angular/router';
import { LoaderService } from './shared/loader/loader.service';

export function initApp(configurationService: ConfigurationService) {
  return () => configurationService.load();
}

@NgModule({
  declarations: [
    AppComponent,
    RegisterComponent,
    LoginComponent,
    HomeComponent,
    UsersComponent,
    LoaderComponent,
    UserDialogComponent,
    NutritionComponent,
    ProductsComponent,
    MealsComponent,
    ProductDetailsComponent,
    MealDetailsComponent,
    DashboardComponent,
    RegisterMealDialogComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    AngularMaterialModule,
    BrowserAnimationsModule,
    FormsModule,
    ReactiveFormsModule,
    MatCardModule,
    HttpClientModule,
    MatSnackBarModule,
    MatProgressSpinnerModule,
    MatMenuModule,
    NgxChartsModule,
    NgSelectModule,
    MatSliderModule,
    MatExpansionModule
  ],
  providers: [
    {
      provide: APP_INITIALIZER,
      useFactory: initApp,
      multi: true,
      deps: [ConfigurationService]
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: TokenInterceptor,
      multi: true
    },
    {
      provide: HTTP_INTERCEPTORS,
      useClass: LoaderInterceptor,
      multi: true
    }
  ],
  bootstrap: [AppComponent],
  entryComponents: [UserDialogComponent, RegisterMealDialogComponent]
})
export class AppModule { }
