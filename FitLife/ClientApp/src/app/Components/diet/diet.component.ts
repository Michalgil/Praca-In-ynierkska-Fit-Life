import { Dimensions } from './../../Models/dimensions';
import { DietService } from './../../services/Diet/diet.service';
import { Observable } from 'rxjs';
import { DietData } from './../../Models/diet.data.';
import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../../services/Product/product.service';

@Component({
  selector: 'app-diet',
  templateUrl: './diet.component.html',
  styleUrls: ['./diet.component.css']
})
export class DietComponent implements OnInit {
  dietForm: FormGroup;
  formError: boolean;
  gender: boolean;
  dietExists: boolean;
  currentDiet: any;
  meals: any[] = [] ;
  numberOfMeal: number;
  products: any[] = [];
  showMeal: boolean;

  constructor(
    //private http: HttpClient,
    private formBuilder: FormBuilder,
    private dietService: DietService,
    private productService: ProductService
  ) { }

  ngOnInit() {
    this.showMeal = false;
    // this.dietService.getCurrentDiet()
    // .subscribe(result => {
    //   console.log(result);
    //   //this.downloadDiet();
    // }, error => {
    //   this.formError = true;
    // });
    this.downloadDiet();
    this.createDietForm();
  }

  createDietForm(){
    this.dietForm = this.formBuilder.group({
      weight: ['', Validators.required],
      height: ['', Validators.required],
      age: ['', Validators.required],
      dailyActivity: ['', Validators.required],
      dietTarget: ['', Validators.required],
      gender: ['', Validators.required],
      arms: ['', Validators.required],
      chest: ['', Validators.required],
      thig: ['', Validators.required],
      waist: ['', Validators.required],
      buttocks: ['', Validators.required],
    });
  }

  createDiet(){
    this.dietService
      .createDiet(this.getDietDataFromForm()) // dodac tutaj model dimensions 
      .subscribe(result => {
        console.log(result);
        this.downloadDiet();
      }, error => {
        this.formError = true;
      });
  }

  downloadDiet(){
    this.dietService.getCurrentDiet()
    .subscribe(result => {
     this.dietExists = true;
     this.currentDiet = result;
     this.meals = this.currentDiet.Meals;
     console.log( this.currentDiet)
     this.changeKcalToGrams()
     this.productService.getProducts()
     .subscribe(result =>{
       this.products = result;
       console.log( this.products);
     },
      error => {
        console.log(error);
      });
    }, error => {
      this.dietExists = false;
      console.log(error);
    });
  }

  getDietDataFromForm() : DietData{
    const dietModel = this.dietForm.value;
    if(dietModel.gender === "Male")
    {
      this.gender = true;
    }
    else
    {
      this.gender = false;
    }
    return new DietData(dietModel.weight as number,dietModel.height as number, dietModel.age as number, dietModel.dailyActivity as number, dietModel.dietTarget as number, this.gender as boolean);
  }

  getDimensionDataFromForm(): Dimensions{
    const dimenionsModel = this.dietForm.value;

    return new Dimensions(dimenionsModel.arms as number, dimenionsModel.waist as number, dimenionsModel.thig as number, dimenionsModel.chest as number, dimenionsModel.buttocks as number);
  }

  changeKcalToGrams()
  {
    this.currentDiet.Protein = (this.currentDiet.Protein/4).toFixed(0);
    this.currentDiet.Fat = (this.currentDiet.Fat/9).toFixed(0);
    this.currentDiet.Carbohydrates = (this.currentDiet.Carbohydrates/4).toFixed(0);
    for (let i = 0; i < this.meals.length; i++) {
      this.meals[i].Protein = (this.meals[i].Protein/4).toFixed(0);
      this.meals[i].Fat = (this.meals[i].Fat/9).toFixed(0);
      this.meals[i].Carbohydrates = (this.meals[i].Carbohydrates/4).toFixed(0);
    }
    
  }

  displayMealTable(numberOfmeal: number)
  {
    this.numberOfMeal = numberOfmeal;
    this.showMeal = true;
  }

}
