import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-calculators',
  templateUrl: './calculators.component.html',
  styleUrls: ['./calculators.component.css']
})
export class CalculatorsComponent implements OnInit {
showCalculatorForm: boolean;
showBmiForm: boolean;
showWhrForm: boolean;
showBmrForm: boolean;
bmiValue: string = "";
bmiResult: number;
weight: number = 0;
age: number =0;
height: number = 0;
bmrValue: number = 0;
bmrResult: string = "";
waist: number = 0;
hips: number = 0;
whrValue: string = "";
whrResult: number = 0;
whrdescribe: string = "";
selectedGender: string = "";
modalRef: BsModalRef;

  constructor(private modalService: BsModalService) {}

  ngOnInit() {
    this.showBmiForm = true;
    this.showWhrForm = true;
    this.showBmrForm = true;
  }

  openModal(template: TemplateRef<any>) {
    this.resetData();
    this.modalRef = this.modalService.show(template);
  }

  calculateBmi(){
    this.bmiResult = this.weight/(this.height * this.height);
    this.bmiValue = this.bmiResult.toFixed(1);
    this.showBmiForm = false;
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
      this.showWhrForm = false;
    }

    calculateBmr(){
      this.showBmrForm = false;
      if(this.selectedGender === "man"){
        this.bmrValue = 66 + (13.7 * this.weight) + (5 * this.height) - (6.76 * this.age);
        console.log( this.bmrValue);
        this.bmrResult = this.bmrValue.toFixed(1);
       }
       else if(this.selectedGender === "woman"){
        this.bmrValue = 655 + (9.7 * this.weight) + (1.8 * this.height) - (4.7 * this.age);
        this.bmrResult = this.bmrValue.toFixed(1);
       }
       console.log(this.bmrResult);
    }

    selectChangeHandler (event: any) {
      this.selectedGender = event.target.value;
    }
    resetData(){
      this.showBmiForm = true;
      this.showWhrForm = true;
      this.showBmrForm = true;
      this.bmiValue = "";
      this.bmiResult = 0;
      this.weight = 0;
      this.age = 0;
      this.height = 0;
      this.bmrValue = 0;
      this.bmrResult = "";
      this.waist = 0;
      this.hips = 0;
      this.whrValue = "";
      this.whrResult = 0;
      this.whrdescribe = "";
      this.selectedGender = "";
    }
}
