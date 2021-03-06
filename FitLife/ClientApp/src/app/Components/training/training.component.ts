import { TrainingService } from './../../services/Training/training.service';
import { FormGroup, FormBuilder, Validators, FormControl } from '@angular/forms';
import { Component, OnInit } from '@angular/core';
import { TrainingData } from '../../Models/training.data';
@Component({
  selector: 'app-training',
  templateUrl: './training.component.html',
  styleUrls: ['./training.component.css']
})
export class TrainingComponent implements OnInit {
  trainingForm: FormGroup;
  formError: boolean;
  crateTrainingForm: boolean;
  showTraining: boolean;
  counter: number;
  numberOfExercise: number;
  trainingPlan: any[] = [];
  numberOfTraining: number;


  constructor(
     private formBuilder: FormBuilder,
     private trainingService: TrainingService
  ) { }

  ngOnInit() {
    this.numberOfExercise = 1;
    this.crateTrainingForm = true;
    this.createTrainingForm();
    this.getTraining();
  }

  createTrainingForm(){
    this.trainingForm = this.formBuilder.group({
      priorityPart: ['', Validators.required],
      experience: ['', Validators.required],
    });
  }

  createTraning(){
    this.trainingService.createTraining(this.getTrainingtDataFromForm())
      .subscribe(result => {
        this.crateTrainingForm = false;
        this.trainingPlan = result;
      }, error => {
        this.formError = true;
      });
  }

  getTraining(){
    this.trainingService
      .getTraining()
      .subscribe(result => {
        if(result.length > 0){
          this.crateTrainingForm = false;
          this.trainingPlan = result;
          console.log(result);
        }
        else{
          this.crateTrainingForm = true;
        }  
      }, error => {
        console.log(error);
      });
  }

  getTrainingtDataFromForm(): TrainingData{
    const trainingModel = this.trainingForm.value;

    return new TrainingData(trainingModel.experience as number, trainingModel.priorityPart as string);
  }

  setTraining(numberOfTraining: number){
    this.showTraining = true;
    this.numberOfTraining = numberOfTraining;
    this.counter = numberOfTraining += 1;
    console.log(this.trainingPlan[this.numberOfTraining]);
  }


  

}
