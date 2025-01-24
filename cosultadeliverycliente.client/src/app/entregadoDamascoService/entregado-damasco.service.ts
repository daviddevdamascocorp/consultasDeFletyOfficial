import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ClientesFlety } from '../clientes.interface';

@Injectable({
  providedIn: 'root'
})
export class EntregadoDamascoService {

   private baseUrl = environment.baseUrl
       constructor(private httpClient:HttpClient) { }
    getTiendasEntregadoDamasco(){
         return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/DamascoEntregado`)
       }
 
       getClientesEntregadoDamascoTienda(  store?: string){
         let params: any = {};
         if (store) params.store = store;
         return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/DamascoEntregado/flotaportienda`,{params})
       }
        getClientesEntregadoDamascoTiendaFecha(startDate?: Date, endDate?: Date,  store?: string){
           let params: any = {};
           
           if (startDate) params.startDate = startDate.toISOString();
           if (endDate) params.endDate = endDate.toISOString();
          
           if (store) params.store = store;
           return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/DamascoEntregado/flotastatustiendafecha`,{params})
         }
 
         getClientesEntregadoDamascoFecha(startDate?: Date, endDate?: Date){
           let params: any = {};
           
           if (startDate) params.startDate = startDate.toISOString();
           if (endDate) params.endDate = endDate.toISOString();
          
          
           return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/DamascoEntregado/flotadamascofecha`,{params})
         }
}
