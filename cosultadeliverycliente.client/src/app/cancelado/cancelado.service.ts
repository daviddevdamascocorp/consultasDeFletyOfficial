import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../environments/environment.development';
import { ClientesFlety } from '../clientes.interface';

@Injectable({
  providedIn: 'root'
})
export class CanceladoService {

  private baseUrl = environment.baseUrl
      constructor(private httpClient:HttpClient) { }

      getCanceladoDamasco(){
              return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/cancelado`)
            }
      
            getClientesCanceladoTienda(  store?: string){
              let params: any = {};
              if (store) params.store = store;
              return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/cancelado/canceladoportienda`,{params})
            }
             getClientesFlotaDamascoTiendaFecha(startDate?: Date, endDate?: Date,  store?: string){
                let params: any = {};
                
                if (startDate) params.startDate = startDate.toISOString();
                if (endDate) params.endDate = endDate.toISOString();
               
                if (store) params.store = store;
                return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/FlotaDamasco/canceladostatustiendafecha`,{params})
              }
      
              getClientesFlotaDamascoFecha(startDate?: Date, endDate?: Date){
                let params: any = {};
                
                if (startDate) params.startDate = startDate.toISOString();
                if (endDate) params.endDate = endDate.toISOString();
               
               
                return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/FlotaDamasco/canceladodamascofecha`,{params})
              }
}
