import { AfterViewInit, Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { MatPaginator } from '@angular/material/paginator';
import { MatSort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ClientesFlety } from '../clientes.interface';
import { DashboardService } from '../dashboard.service';
import { DetalleClienteComponent } from '../detalle-cliente/detalle-cliente.component';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { MyErrorStateMatcher } from '../matcher';
import { StatusFlete } from '../status-etapas.interface';
import { DamascoTiendas } from '../tiendas.interface';

@Component({
  selector: 'app-delivery-cancelado',
  templateUrl: './delivery-cancelado.component.html',
  styleUrl: './delivery-cancelado.component.css'
})
export class DeliveryCanceladoComponent implements OnInit,AfterViewInit {


 

  displayedColumns: string[] = ['numFactura', 'codCliente', 'nomCliente', 'status', 'sucursal', 'direccion', 'fechaFactura', 'fechaActualizacion','detalle'];
   TablaClientes!:MatTableDataSource<ClientesFlety>
   @ViewChild(MatSort) sort!: MatSort;
  @ViewChild(MatPaginator) paginator: MatPaginator | any;
  recordForm: FormGroup;
    matcher = new MyErrorStateMatcher();
    damascoTiendas!:DamascoTiendas[]
   statusFletes: StatusFlete[] = [
    {value: 'Pendiente', viewValue: 'Pendiente'},
     {value: 'En curso', viewValue: 'En curso'},
     {value: 'Entregado', viewValue: 'Entregado'},
     {value: 'Flota Damasco', viewValue: 'Flota Damasco'},
     {value: 'Cancelado', viewValue: 'Cancelado'},
   ];

 
   /**
    *
    */
   constructor(private dashboardService:DashboardService,public dialog: MatDialog,private formBuilder: FormBuilder) {
    this.recordForm = this.formBuilder.group({
      startDate: ['',Validators.required],
      endDate: ['',Validators.required],
      status: [''],
      store: [''],
    });
     
   }
   ngAfterViewInit(): void {
     this.dashboardService.getClientesFlety().subscribe(results=>{
      console.log(results)
         this.TablaClientes = new MatTableDataSource(results)
         this.TablaClientes.paginator = this.paginator;
         this.TablaClientes.sort = this.sort;
     })
   }
   ngOnInit(): void {
     this.dashboardService.getTiendasDamasco().subscribe(results=>{
      this.damascoTiendas = results
     })
   }

   detalleDialogo(facutra:string,sucursal:string):void{
    console.log(facutra)
    const dialogRef = this.dialog.open(DetalleClienteComponent, {
      data: { NumFactura: facutra, IdSucursal: sucursal } 
    });
    dialogRef.afterClosed().subscribe(result => {
      console.log('Dialog result:', result);
    });
   }
  applyFilter(event: Event) {
     const filterValue = (event.target as HTMLInputElement).value;
     this.TablaClientes.filter = filterValue.trim().toLowerCase();
     if (this.TablaClientes.paginator) {
       this.TablaClientes.paginator.firstPage();
     }
   }

   filterRecords(){
    const { startDate, endDate, status, store } = this.recordForm.value;

   
    const filters = {
      startDate: startDate,
      endDate: endDate
    };
    
    if (status && store) {
      this.dashboardService.getClientesStatusTiendaFecha( startDate, endDate, status, store ).subscribe(results=>{
        console.log(results)
           this.TablaClientes = new MatTableDataSource(results)
           this.TablaClientes.paginator = this.paginator;
           this.TablaClientes.sort = this.sort;
       })
    }
    else if (store) {
      this.dashboardService.getClientesTiendaFecha( startDate, endDate,  store ).subscribe(results=>{
        console.log(results)
           this.TablaClientes = new MatTableDataSource(results)
           this.TablaClientes.paginator = this.paginator;
           this.TablaClientes.sort = this.sort;
       })
    
    } else if (status){
      this.dashboardService.getClientesStatusFecha( startDate, endDate, status).subscribe(results=>{
        console.log(results)
           this.TablaClientes = new MatTableDataSource(results)
           this.TablaClientes.paginator = this.paginator;
           this.TablaClientes.sort = this.sort;
       })
    
    }else{
      this.dashboardService.getClientesFecha( startDate, endDate).subscribe(results=>{
        console.log(results)
           this.TablaClientes = new MatTableDataSource(results)
           this.TablaClientes.paginator = this.paginator;
           this.TablaClientes.sort = this.sort;
       })
    }
   /* this.dashboardService.getClientesStatusTiendaFecha( startDate, endDate, status, store ).subscribe(results=>{
      console.log(results)
         this.TablaClientes = new MatTableDataSource(results)
         this.TablaClientes.paginator = this.paginator;
         this.TablaClientes.sort = this.sort;
     })*/
   }
}
