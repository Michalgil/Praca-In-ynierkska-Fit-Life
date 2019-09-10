import { Component, OnInit } from '@angular/core';

@Component({
  selector: 'app-home-page',
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.css']
})
export class HomePageComponent implements OnInit {
  sliderImages:  any[];
  constructor() { 
    
  }

  ngOnInit() {
    this.sliderImages = [{
      url: '../../assets/Images/Jay-Cutler-400x400.png'
      }, {
        url: '../../assets/Images/5cal.JPG'
      }, {
        url: '../../assets/Images/6cal.JPG'
      },];

  }

}
