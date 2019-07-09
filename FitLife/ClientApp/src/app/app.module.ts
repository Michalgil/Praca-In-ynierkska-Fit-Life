import { AuthGuard } from './guards/auth.guard';
import { AccountService } from './services/Account/account.service';
import { BrowserModule } from '@angular/platform-browser';
import { NgModule, Component } from '@angular/core';
import { ReactiveFormsModule,FormsModule  } from '@angular/forms';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { RouterModule } from '@angular/router';
import { AppRoutingModule } from './app-routing.module';

import { AppComponent } from './app.component';
import { NavMenuComponent } from './Components/nav-menu/nav-menu.component';
import { HomePageComponent } from './Components/home-page/home-page.component';
import { CalculatorsComponent } from './Components/calculators/calculators.component';
import { DietComponent } from './Components/diet/diet.component';
import { TrainingComponent } from './Components/training/training.component';
import { ProgressComponent } from './Components/progress/progress.component';
import { LoginComponent } from './components/login/login.component';
import { RegisterComponent } from './components/register/register.component';

@NgModule({
  declarations: [
    AppComponent,
    NavMenuComponent,
    HomePageComponent,
    CalculatorsComponent,
    DietComponent,
    TrainingComponent,
    ProgressComponent,
    LoginComponent,
    RegisterComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    HttpClientModule,
    ReactiveFormsModule,
    FormsModule,
    AppRoutingModule
  ],
  providers: [
    AccountService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
