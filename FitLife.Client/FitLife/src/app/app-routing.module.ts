import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { RegisterComponent } from './authentication/register/register.component';
import { LoginComponent } from './authentication/login/login.component';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from './shared/guards/auth.guard';
import { UsersComponent } from './users/users.component';
import { NutritionComponent } from './nutrition/nutrition.component';
import { ProductDetailsComponent } from './nutrition/products/product-details/product-details.component';
import { MealDetailsComponent } from './nutrition/meals/meal-details/meal-details.component';
import { MealCategoriesResolver } from './shared/resolvers/mealCategories.resolver';
import { ProductsResolver } from './shared/resolvers/products.resolver';
import { DashboardComponent } from './dashboard/dashboard.component';


const routes: Routes = [
  { path: '', redirectTo: 'login', pathMatch: 'full' },
  { path: 'register', component: RegisterComponent },
  { path: 'login', component: LoginComponent },
  { path: 'home', component: HomeComponent, canActivate: [AuthGuard] },
  {
    path: 'users', component: UsersComponent, canActivate: [AuthGuard],
    data: { permittedRoles: ['Admin'] }
  },
  { path: 'nutrition', component: NutritionComponent, canActivate: [AuthGuard] },
  { path: 'nutrition:tab', component: NutritionComponent, canActivate: [AuthGuard] },
  { path: 'nutrition/product', component: ProductDetailsComponent, canActivate: [AuthGuard] },
  { path: 'nutrition/product/:id', component: ProductDetailsComponent, canActivate: [AuthGuard] },
  {
    path: 'nutrition/meal', component: MealDetailsComponent, canActivate: [AuthGuard],
    resolve: { mealCategories: MealCategoriesResolver, products: ProductsResolver }
  },
  {
    path: 'nutrition/meal/:id', component: MealDetailsComponent, canActivate: [AuthGuard],
    resolve: { mealCategories: MealCategoriesResolver, products: ProductsResolver }
  },
  { path: 'dashboard', component: DashboardComponent, canActivate: [AuthGuard] },


];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
