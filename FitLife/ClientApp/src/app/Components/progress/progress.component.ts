import { DietData } from './../../Models/diet.data.';
import { Dimensions } from './../../Models/dimensions';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { DietService } from '../../services/Diet/diet.service';
import { TrainingService } from '../../services/Training/training.service';
import { ProgressService } from '../../services/Progress/progress.service';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { Router } from '@angular/router';

@Component({
  selector: 'app-progress',
  templateUrl: './progress.component.html',
  styleUrls: ['./progress.component.css']
})
export class ProgressComponent implements OnInit {
  modalRef: BsModalRef;
  showDimensions: boolean;
  updateDimensions: boolean;
  dietData: any;
  counter: number;
  successcounter: number;
  target: string;
  numberOfTraining: number;
  dimensions: any;
  cos: any;
  selectedTarget: any;
  weight: number;
  progressForm: FormGroup;

  constructor(
    private dietService: DietService,
    private trainingService: TrainingService,
    private progressService: ProgressService,
    private modalService: BsModalService,
    private formBuilder: FormBuilder,
    private router: Router
  ) { }

  ngOnInit() {
    this.showDimensions = false;
    this.updateDimensions = false;
    this.successcounter = 0;
    this.counter = 0;
    this.getDiet();
    this.getQuntityOfTrainings();
    this.getDimensions();
    this.createProgressForm();





  }

  createProgressForm(){
    this.progressForm = this.formBuilder.group({
      weight: ['', Validators.required],
      dietTarget: ['', Validators.required],
      arms: ['', Validators.required],
      chest: ['', Validators.required],
      thig: ['', Validators.required],
      waist: ['', Validators.required],
      buttocks: ['', Validators.required],
    });
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getDiet(){
    this.dietService.getCurrentDiet()
    .subscribe(result => {
      this.dietData = result;
      this.weight = result.Weight;
      this.checkDietTarget(this.dietData);
      this.ifExists();
    }, error => {
      console.log(error);
    });
  }

  getQuntityOfTrainings(){
    this.trainingService.getQunatityoFTrainings()
    .subscribe(result => {
      if(result == 3 || result == 6){
        this.numberOfTraining = 3
      }
      else if(result == 4 || result == 8){
        this.numberOfTraining = 4
      }else if(result == 5 || result == 10){
        this.numberOfTraining = 5
      }
      if(result > 0 )
      {
        this.ifExists();
      }
      
    }, error => {
      console.log(error);
    });
  }

  getDimensions(){
    this.progressService.getCurrentDimensions()
    .subscribe(result => {
      this.dimensions = result;
      console.log(result)
    }, error => {
      console.log(error);
    });
  }

 ifExists(){
  this.counter = this.counter + 1;
 }

 checkDietTarget(dietData: any){

  if(dietData.WeightReduction == true){
    this.target = "Redukcja wagi"
  }
  else if(dietData.Mass == true){
    this.target = "Nabranie masy"
  }
  else if(dietData.WeightMaintenance == true){
    this.target = "Utrzymanie wagi"
  }
 }

 sendData(event: any){
  this.dietService.updateDiet(this.getDietDataFromForm())
  .subscribe(result => {
    this.successcounter = this.successcounter + 1;
    this.trainingService.updateTraining()
    .subscribe(result => {
      this.successcounter = this.successcounter + 1;
      if(this.successcounter == 2){
        this.showDimensions = false;
      }
      console.log(result)
    }, error => {
      console.log(error);
    });
    console.log(result)
  }, error => {
    console.log(error);
  });

  }

  dimension(){
    this.showDimensions = true;
    this.updateDimensions = false;
  }

  newDimension(){
    this.updateDimensions = true;
  }
  goToDietView(){
    this.router.navigate(['/diet']);
  }
  goToTrainingView(){
    this.router.navigate(['/training']);
  }
  selectChangeHandler (event: any) {
    this.selectedTarget = event.target.value;
  }
  getDietDataFromForm(): DietData {
    const dietModel = this.progressForm.value;
    return new DietData(dietModel.weight as number, 0 , 0 , 0 , dietModel.dietTarget as number, dietModel.arms as number, dietModel.waist as number, dietModel.thig as number, dietModel.chest as number, dietModel.buttocks as number);
  }
}
