import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { ApisService } from '../Services/apis.service';
import { FormsModule } from '@angular/forms';
import { FormProduct } from '../Interfaces/form-product';
import { Product } from '../Interfaces/product';
import { GetProductResponse } from '../Interfaces/get-product-response';
import { ProductItemComponent } from '../product-item/product-item.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-producto',
  standalone: true,
  imports: [NavMenuComponent,FormsModule,ProductItemComponent, CommonModule],
  templateUrl: './producto.component.html',
  styleUrl: './producto.component.css'
})
export class ProductoComponent implements OnInit {
  constructor (private Api: ApisService){}

  inicialForm : FormProduct = {idProducto:0, nombre:'', descripcion:'', precio1:0,precio2:999999999.99,stock1:0,stock2:999999,fechaDeAct1:'1999-12-31',fechaDeAct2:'1999-12-31',fechaDeCreacion1:'1999-12-31',fechaDeCreacion2:'1999-12-31'}

  ngOnInit(): void {
    this.GetProducts(this.inicialForm)
  }

  ListaProductos: Product[] = []

  GetProducts(Form : FormProduct){
    
    Form.fechaDeAct1 = '1999-12-31'
    Form.fechaDeAct2 = '1999-12-31'
    Form.fechaDeCreacion1 = '1999-12-31'
    Form.fechaDeCreacion2 = '1999-12-31'
    
    this.Api.TraerProductos(Form).subscribe((Result : GetProductResponse) => {
      this.ListaProductos = Result.result
      console.log(this.ListaProductos)
    })
  }

}
