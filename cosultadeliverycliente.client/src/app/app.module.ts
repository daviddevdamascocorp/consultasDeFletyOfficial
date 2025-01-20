import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
import {MatFormFieldModule} from '@angular/material/form-field';
import { MaterialModule } from './material/material.module';
import { MatTableModule } from '@angular/material/table';
import { MatInputModule } from '@angular/material/input';
import { DetalleClienteComponent } from './detalle-cliente/detalle-cliente.component';
import { DeliveryPendienteComponent } from './delivery-pendiente/delivery-pendiente.component';
import { DeliveryEntregadoComponent } from './delivery-entregado/delivery-entregado.component';
import { DeliveryDamascoComponent } from './delivery-damasco/delivery-damasco.component';
import { DeliveryCanceladoComponent } from './delivery-cancelado/delivery-cancelado.component';
import { ReactiveFormsModule } from '@angular/forms';
import { DeliveryEnCursoComponent } from './delivery-en-curso/delivery-en-curso.component';
@NgModule({
  declarations: [
    AppComponent,
    DetalleClienteComponent,
    DeliveryPendienteComponent,
    DeliveryEntregadoComponent,
    DeliveryDamascoComponent,
    DeliveryCanceladoComponent,
    DeliveryEnCursoComponent,
  
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, MatSlideToggleModule,MaterialModule,
    MatTableModule,MatFormFieldModule,  MatInputModule,ReactiveFormsModule

  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
