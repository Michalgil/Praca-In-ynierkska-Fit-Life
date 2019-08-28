import { AuthGuard } from './guards/auth.guard';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule, CanActivate } from '@angular/router';
import { CalculatorsComponent } from './Components/calculators/calculators.component';
import { DietComponent } from './Components/diet/diet.component';
import { TrainingComponent } from './Components/training/training.component';
import { ProgressComponent } from './Components/progress/progress.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

const routes: Routes = [
  { path: '', component: HomePageComponent },
  { path: 'calculators', component: CalculatorsComponent },
  { path: 'diet', component: DietComponent },
  { path: 'training', component: TrainingComponent},//, canActivate: [AuthGuard] 
  { path: 'progress', component: ProgressComponent, canActivate: [AuthGuard] },
  { path: 'login', component: LoginComponent},
  { path: 'register', component: RegisterComponent },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
