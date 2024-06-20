import { Component,Input } from '@angular/core';
import { Product } from '../Interfaces/product';
import { ApisService } from '../Services/apis.service';

@Component({
  selector: 'app-product-item',
  standalone: true,
  imports: [],
  templateUrl: './product-item.component.html',
  styleUrl: './product-item.component.css'
})
export class ProductItemComponent {
  @Input() Product!: Product;

  constructor (private Api: ApisService){}

  BorrarProducto(){
    this.Api.DeleteProductos(this.Product.idProducto).subscribe(() =>{
      this.Product.nombre= '#####'
      this.Product.idProducto= 0
      this.Product.precio = 0
      this.Product.stock = 0
    })
  }

}
