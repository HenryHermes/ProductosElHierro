import { Component } from '@angular/core';
import { NavMenuComponent } from '../nav-menu/nav-menu.component';
import { FormsModule } from '@angular/forms';
import { ApisService } from '../Services/apis.service';
import { Product } from '../Interfaces/product';
import { from } from 'rxjs';
import { ActionResponse } from '../Interfaces/action-response';

@Component({
  selector: 'app-crear-producto',
  standalone: true,
  imports: [NavMenuComponent,FormsModule],
  templateUrl: './crear-producto.component.html',
  styleUrl: './crear-producto.component.css'
})
export class CrearProductoComponent {
  constructor(private Api: ApisService){}

  Product : Product = {idProducto : 0, nombre : '', descripcion : '', precio:0,stock:0, fechaDeAct: '1999-12-31', fechaDeCreacion: '1999-12-31', activo: true}

  Ejecucion = ''

  CreateOrUpdate(Form: any){
    
    this.Product.idProducto = Form.idProducto
    this.Product.nombre = Form.nombre
    this.Product.descripcion = Form.descripcion
    this.Product.precio = Form.precio
    this.Product.stock = Form.stock

    if(Form.Operacion == "1"){
      this.Api.UpdateProductos(this.Product).subscribe((Result:ActionResponse)=>{
        if(Result.success == true){
          this.Ejecucion='Se ha realizado exitosamente'
        }else{
          this.Ejecucion=''
        }
      })
    }else{
      this.Api.InsertarProductos(this.Product).subscribe((Result:ActionResponse)=>{
        if(Result.success == true){
          this.Ejecucion='Se ha realizado exitosamente'
        }else{
          this.Ejecucion=''
        }
      })
    }

  }
}
