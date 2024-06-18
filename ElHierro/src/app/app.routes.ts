import { RouterModule, Routes } from '@angular/router';
import { ProductoComponent } from './producto/producto.component';
import { NgModule } from '@angular/core';
import { LogingComponent } from './loging/loging.component';
import { UsuariosComponent } from './usuarios/usuarios.component';
import { CrearUsuariosComponent } from './crear-usuarios/crear-usuarios.component';
import { CrearProductoComponent } from './crear-producto/crear-producto.component';

export const routes: Routes = [
    {
        path: '',
        component: LogingComponent
    },
    {
        path: 'Producto',
        component: ProductoComponent
    },
    {
        path: 'Usuarios',
        component: UsuariosComponent
    },
    {
        path: 'CrearUsuarios',
        component: CrearUsuariosComponent
    },
    {
        path: 'CrearProducto',
        component: CrearProductoComponent
    }

];

@NgModule({
    imports: [RouterModule.forRoot(routes)],
    exports: [RouterModule]
})
export class AppRoutingModele {}