import { Component, Inject, inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { DashboardService } from '../dashboard.service';
import { Articulo, DetalleCliente } from '../detalle-cliente.interface';
import { MatTableDataSource } from '@angular/material/table';
import { MyErrorStateMatcher } from '../matcher';
import { StatusFlete } from '../status-etapas.interface';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { StatusActualizacion } from '../actualizar-estatus.interface';
import { MatSelectChange } from '@angular/material/select';
interface DialogData {
  NumFactura: string;
  IdSucursal: string;
}
@Component({
  selector: 'app-detalle-cliente',
  templateUrl: './detalle-cliente.component.html',
  styleUrl: './detalle-cliente.component.css'
})

export class DetalleClienteComponent implements OnInit {
 
  detalleCliente!:DetalleCliente
  displayedColumns: string[] = ['codArticulo', 'descripcion', 'cantidadArticulo'];
 
  TablaProductos!:MatTableDataSource<Articulo>
  matcher = new MyErrorStateMatcher();
  /**
   *
   */

  statusFletes: StatusFlete[] = [
    {value: 'En curso', viewValue: 'En curso'},
    {value: 'Entregado', viewValue: 'Entregado'},
    {value: 'Entregado Damasco', viewValue: 'Entregado Damasco'},
    {value: 'Flota Damasco', viewValue: 'Flota Damasco'},
    {value: 'Cancelado', viewValue: 'Cancelado'},
  ];
  constructor(private dashboardService:DashboardService,  @Inject(MAT_DIALOG_DATA) public data: DialogData,) {

    console.log('Data received in dialog:', this.data);
  }
  today = new Date(); 
  bodyUpdate!:StatusActualizacion
  ngOnInit(): void {
    console.log('Data received in dialog:', this.data);
    console.log(this.data.NumFactura)
    this.dashboardService.getClienteDetallado(this.data.NumFactura,this.data.IdSucursal).subscribe(results =>{
      this.detalleCliente = results
      this.TablaProductos = new MatTableDataSource(this.detalleCliente.articulos)
      console.log(this.detalleCliente)
    }
    
    )
  }
  stuatusForm = new FormGroup({
    selectedOption: new FormControl(''),
    myDate: new FormControl(this.today, Validators.required),
  });
  ActualizarCliente(ev: MatSelectChange,factura:string){
  console.log(this.today)
   // this.bodyUpdate.fechaActualizacion = 
   console.log(ev)
    let data ={
      Status:ev.value,
      fechaActualizacion: this.today,
     Tienda: this.detalleCliente.tienda
    }
    console.log(data)
      console.log(this.stuatusForm.value + " " + factura)
      this.dashboardService.updateStatus(data,factura).subscribe(result =>{window.location.reload()})
  }

}
