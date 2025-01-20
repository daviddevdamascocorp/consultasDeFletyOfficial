import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ClientesFlety } from '../clientes.interface';

@Injectable({
  providedIn: 'root'
})
export class EntregadoService {

  private baseUrl = environment.baseUrl
        constructor(private httpClient:HttpClient) { }
     getEntregadoDamasco(){
          return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/entregado`)
        }
  
        getTiendaEntregado(  store?: string){
          let params: any = {};
          if (store) params.store = store;
          return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/entregado/entregadoportienda`,{params})
        }
         getTiendaEntregadoFecha(startDate?: Date, endDate?: Date,  store?: string){
            let params: any = {};
            
            if (startDate) params.startDate = startDate.toISOString();
            if (endDate) params.endDate = endDate.toISOString();
           
            if (store) params.store = store;
            return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/entregado/entregadotiendafecha`,{params})
          }
  
          getTiendaFecha(startDate?: Date, endDate?: Date){
            let params: any = {};
            
            if (startDate) params.startDate = startDate.toISOString();
            if (endDate) params.endDate = endDate.toISOString();
           
           
            return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/entregado/entregadofecha`,{params})
          }
}
