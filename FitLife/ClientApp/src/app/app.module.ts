import { TrainingService } from './services/Training/training.service';
import { ProductService } from './services/Product/product.service';
import { DietService } from './services/Diet/diet.service';
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
import {NgxPaginationModule} from 'ngx-pagination';
import { Ng2SearchPipeModule } from 'ng2-search-filter';

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
    NgxPaginationModule,
    Ng2SearchPipeModule,
    AppRoutingModule
  ],
  providers: [
    AccountService,
    DietService,
    ProductService,
    TrainingService,
    AuthGuard
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
