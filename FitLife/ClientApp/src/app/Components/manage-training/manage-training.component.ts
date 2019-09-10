import { Training } from './../../Models/training';
import { TrainingService } from './../../services/Training/training.service';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-manage-training',
  templateUrl: './manage-training.component.html',
  styleUrls: ['./manage-training.component.css']
})
export class ManageTrainingComponent implements OnInit {
exerciseList: Training[] = [];
exCategory: string = "Klatka piersiowa";
exName: string = "";
modalRef: BsModalRef;

  constructor(
    private trainingService: TrainingService,
    private modalService: BsModalService
  ) { }

  ngOnInit() {
   this.getExercieses()
  }
  
  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  getExercieses(){
    this.trainingService
      .getExercises()
      .subscribe(result => {
        result.forEach(element => {
          var ex = new Training(element.Name,element.PartOfBody.Name);
          this.exerciseList.push(ex);
        });
      }, error => {
        console.log(error);
      });
  }

  Remove(cwiczenie: Training){
    const index: number = this.exerciseList.indexOf(cwiczenie);
    if (index !== -1) {
      this.exerciseList.splice(index,1);
    }
  }

  AddExercise(){
    var ex = new Training(this.exName,this.exCategory);
    this.exerciseList.push(ex);
    this.exName = "";
  }

  selectChangeHandler (event: any) {
    this.exCategory = event.target.value;
  }

  

}
