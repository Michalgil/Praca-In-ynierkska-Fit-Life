import { AccountService } from './../../services/Account/account.service';
import { MealList } from './../../Models/mealList';
import { Dimensions } from './../../Models/dimensions';
import { DietService } from './../../services/Diet/diet.service';
import { Observable } from 'rxjs';
import { DietData } from './../../Models/diet.data.';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { HttpClient } from '@angular/common/http';
import { ProductService } from '../../services/Product/product.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Product } from '../../Models/product';
import { ChartOptions, ChartType, ChartDataSets } from 'chart.js';
import { Label } from 'ng2-charts';
import * as pluginDataLabels from 'chartjs-plugin-datalabels';


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
  meals: any[] = [];
  numberOfMeal: number;
  products: any[] = [];
  showMeal: boolean;
  showComponent: boolean;
  selectedProduct: Product[] = [];
  lista: Product[] = [];
  modalRef: BsModalRef;
  proteinType: boolean;
  fatType: boolean;
  carbohydratesType: boolean;
  showChart: boolean;
  message: string = "";
  protein: string = "";

  constructor(
    //private http: HttpClient,
    private formBuilder: FormBuilder,
    private dietService: DietService,
    private productService: ProductService,
    private accountService: AccountService,
    private modalService: BsModalService
  ) { }


  ngOnInit() {
    this.showMeal = false;
    this.showChart = false;
    this.showComponent = false;
    this.getDiet();
    this.createDietForm();
    this.proteinType = true;
    this.fatType = true;
    this.carbohydratesType = true;
  }

  showHistory(){
    this.showChart = true;
  }

  createDietForm() {
    this.dietForm = this.formBuilder.group({
      weight: ['', Validators.required],
      height: ['', Validators.required],
      age: ['', Validators.required],
      dailyActivity: ['', Validators.required],
      dietTarget: ['', Validators.required],
      arms: ['', Validators.required],
      chest: ['', Validators.required],
      thig: ['', Validators.required],
      waist: ['', Validators.required],
      buttocks: ['', Validators.required],
    });
  }

  createDiet() {
    this.dietService
      .createDiet(this.getDietDataFromForm())
      .subscribe(result => {
        this.showComponent = true;
        this.dietExists = true;
        this.currentDiet = result;
        this.meals = this.currentDiet.Meals;
        console.log(this.currentDiet)
        this.changeKcalToGrams()
        this.productService.getProducts()
          .subscribe(result => {
            this.products = result;
            console.log(this.products);
          },
            error => {
              console.log(error);
            });
      }, error => {
        this.dietExists = false;
        this.showComponent = true;
        console.log(error);
      });
  }

  getDiet() {
    this.dietService.getCurrentDiet()
      .subscribe(result => {
        this.showComponent = true;
        this.dietExists = true;
        this.currentDiet = result;
        this.meals = this.currentDiet.Meals;
        console.log(this.currentDiet)
        this.changeKcalToGrams()
        this.productService.getProducts()
          .subscribe(result => {
            this.products = result;
            this.lista = result;

            console.log(this.products);
          },
            error => {
              console.log(error);
            });
      }, error => {
        this.dietExists = false;
        this.showComponent = true;
        console.log(error);
      });
  }

  getDietDataFromForm(): DietData {
    const dietModel = this.dietForm.value;
    return new DietData(dietModel.weight as number, dietModel.height as number, dietModel.age as number, dietModel.dailyActivity as number, dietModel.dietTarget as number, dietModel.arms as number, dietModel.waist as number, dietModel.thig as number, dietModel.chest as number, dietModel.buttocks as number);
  }

  changeKcalToGrams() {
    this.currentDiet.Protein = (this.currentDiet.Protein / 4).toFixed(0);
    this.currentDiet.Fat = (this.currentDiet.Fat / 9).toFixed(0);
    this.currentDiet.Carbohydrates = (this.currentDiet.Carbohydrates / 4).toFixed(0);
    for (let i = 0; i < this.meals.length; i++) {
      this.meals[i].Protein = (this.meals[i].Protein / 4).toFixed(0);
      this.meals[i].Fat = (this.meals[i].Fat / 9).toFixed(0);
      this.meals[i].Carbohydrates = (this.meals[i].Carbohydrates / 4).toFixed(0);
    }

  }

  displayMealTable(numberOfmeal: number) {
    this.proteinType = true;
    this.fatType = true;
    this.carbohydratesType = true;
    this.numberOfMeal = numberOfmeal;
    this.showMeal = true;
    this.selectedProduct = [];
  }

  addProductToMealList(produkcik: Product) {
    this.selectedProduct.push(produkcik);
    if (produkcik.Category.match("Białko")) {
      this.proteinType = false;
    } else if (produkcik.Category.match("Tłuszcze")) {
      this.fatType = false;
    } else if (produkcik.Category.match("Węglowodany")) {
      this.carbohydratesType = false;
    }
  }
  
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }
  deleteProduct(produkcik: Product) {
    const index: number = this.selectedProduct.indexOf(produkcik);
    if (index !== -1) {
      this.selectedProduct.splice(index, 1);
      if (produkcik.Category.match("Białko")) {
        this.proteinType = true;
      } else if (produkcik.Category.match("Tłuszcze")) {
        this.fatType = true;
      } else if (produkcik.Category.match("Węglowodany")) {
        this.carbohydratesType = true;
      }

    }
  }
  sendMessageMeal(){
    var name: string;
    var nutritionalValue: string;
    var message2: string = "";
    var protein = this.meals[this.numberOfMeal].Protein;
    var fat = this.meals[this.numberOfMeal].Fat;
    var carbohydrates = this.meals[this.numberOfMeal].Carbohydrates;

    if(this.meals.length = 5){
      if(this.numberOfMeal == 0){
        message2 = 'Śniadanie' + '\n';
      } else if(this.numberOfMeal == 1){
        message2 = 'Obiad' + '\n';
      } else if(this.numberOfMeal == 2){
        message2 = 'Posiłek przed treningowy' + '\n';
      } else if(this.numberOfMeal == 3){
        message2 = 'Posiłek po treningowy' + '\n';
      } else if(this.numberOfMeal == 4){
        message2 = 'Kolacja' + '\n';
      }
    } else{
      if(this.numberOfMeal == 0){
        message2 = 'Śniadanie' + '\n';
      } else if(this.numberOfMeal == 1){
        message2 = 'Posiłek przed treningowy' + '\n';
      } else if(this.numberOfMeal == 2){
        message2 = 'Posiłek po treningowy' + '\n';
      } else if(this.numberOfMeal == 3){
        message2 = 'Kolacja' + '\n';
      }
    }
    
    this.selectedProduct.forEach(function (value){
      name = value.Name;
      if (value.Category.match("Białko")) {
        nutritionalValue =  (((protein * 4)/(value.NutritionalValue)) * 100).toFixed(0);
      } else if (value.Category.match("Tłuszcze")) {
        nutritionalValue = (((fat * 9)/(value.NutritionalValue)) * 100).toFixed(0);
      } else if (value.Category.match("Węglowodany")) {
        nutritionalValue = (((carbohydrates* 4)/(value.NutritionalValue)) * 100).toFixed(0);
      }
      nutritionalValue += 'g'
      message2 += name + ' ' + nutritionalValue + ' ' + '\n';
    })

    let meal = { "Message" : message2};
    this.accountService.sendMeal(meal)
      .subscribe(result => {
      }, error => {
        console.log(error);
      });
  }

}
