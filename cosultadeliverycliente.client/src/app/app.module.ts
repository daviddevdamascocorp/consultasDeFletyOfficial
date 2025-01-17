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
@NgModule({
  declarations: [
    AppComponent,
    DetalleClienteComponent,
  
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, MatSlideToggleModule,MaterialModule,
    MatTableModule,MatFormFieldModule,  MatInputModule

  ],
  providers: [
    provideAnimationsAsync()
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
