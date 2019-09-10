import { Product } from './../../Models/product';
import { Component, OnInit, TemplateRef } from '@angular/core';
import { ProductService } from '../../services/Product/product.service';
import { DeclareVarStmt } from '@angular/compiler';
import { BsModalService, BsModalRef } from 'ngx-bootstrap/modal';

@Component({
  selector: 'app-manage-diet',
  templateUrl: './manage-diet.component.html',
  styleUrls: ['./manage-diet.component.css']
})
export class ManageDietComponent implements OnInit {
products: Product[];
allProducts: Product[];
show: boolean;
modalRef: BsModalRef;
productCategory: string = "Białko"
kcalPerProduct: number;
productName: string;


  constructor(
    private productService: ProductService,
    private modalService: BsModalService) { }

  ngOnInit() {
  this.GetProducts();
  this.products = new Array();
  this.show = true;
  }

  openModal(template: TemplateRef<any>) {
    this.modalRef = this.modalService.show(template);
  }

  GetProducts(){
    this.productService.getProducts()
    .subscribe(result => {
      this.products = result.product;
    },
      error => {
        console.log(error);
      });
  }

  selectChangeHandler (event: any) {
    this.productCategory = event.target.value;
    console.log(this.productCategory );
  }

  Remove(produkcik: Product){
    const index: number = this.products.indexOf(produkcik);
    if (index !== -1) {
      this.products.splice(index,1);
    }
  }
  AddProduct(){
    var product = new Product(0,this.productName,this.kcalPerProduct,this.productCategory);
    this.products.push(product);
    this.productName = "";
    this.kcalPerProduct = 0;
  }
  

}
