import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-calculators',
  templateUrl: './calculators.component.html',
  styleUrls: ['./calculators.component.css']
})
export class CalculatorsComponent implements OnInit {
showCalculatorForm: boolean;
bmiValue: string = "";
bmiResult: number;
weight: number = 0;
height: number = 0;
bmrValue: number = 0;
bmrResult: number = 0;
waist: number = 0;
hips: number = 0;
whrValue: string = "";
whrResult: number = 0;
whrdescribe: string = "";
selectedGender: string = "";


  constructor() {
    this.showCalculator();
    this.resetValues();
   }

  ngOnInit() {
  }

  showCalculator(){
    this.showCalculatorForm = true;
  }
  calculateBmi(){
    this.bmiResult = this.weight/(this.height * this.height);
    this.bmiValue = this.bmiResult.toFixed(1);
  }
  calculateBmr(){
    this.bmrValue = this.weight * 24;
  }
  calculateWhr(){
    this.whrResult = this.waist / this.hips
    this.whrValue = this.whrResult.toFixed(1);

    if(this.whrResult > 1 && this.selectedGender === "man"){
      this.whrdescribe = "Twoj typ sylwetki jest androidalny(tzw.jabłko)";
    }
    else if(this.whrResult < 1 && this.selectedGender === "man"){
      this.whrdescribe = "Twoj typ sylwetki jest gynoidalny(tzw.gruszka)";
    }
    else if(this.whrResult > 0.8 && this.selectedGender === "woman"){
      this.whrdescribe = "Twoj typ sylwetki jest androidalny(tzw.jabłko)"
    }
    else if(this.whrResult < 0.8 && this.selectedGender === "woman"){
      this.whrdescribe = "Twoj typ sylwetki jest gynoidalny(tzw.gruszka)";
    }
  }

  selectChangeHandler (event: any) {
    this.selectedGender = event.target.value;
  }
  resetValues(){
    this.weight = 0;
    this.height = 0;
    this.waist = 0;
    this.hips = 0;
    this.whrdescribe = "";
  }

}
