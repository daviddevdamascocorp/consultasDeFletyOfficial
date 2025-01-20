import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ClientesFlety } from '../clientes.interface';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class FlotaDamascoService {

  private baseUrl = environment.baseUrl
      constructor(private httpClient:HttpClient) { }
   getTiendasFlotaDamasco(){
        return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/FlotaDamasco`)
      }

      getClientesFlotaDamascoTienda(  store?: string){
        let params: any = {};
        if (store) params.store = store;
        return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/FlotaDamasco/flotaportienda`,{params})
      }
       getClientesFlotaDamascoTiendaFecha(startDate?: Date, endDate?: Date,  store?: string){
          let params: any = {};
          
          if (startDate) params.startDate = startDate.toISOString();
          if (endDate) params.endDate = endDate.toISOString();
         
          if (store) params.store = store;
          return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/FlotaDamasco/flotastatustiendafecha`,{params})
        }

        getClientesFlotaDamascoFecha(startDate?: Date, endDate?: Date){
          let params: any = {};
          
          if (startDate) params.startDate = startDate.toISOString();
          if (endDate) params.endDate = endDate.toISOString();
         
         
          return this.httpClient.get<ClientesFlety[]>(`${this.baseUrl}/api/FlotaDamasco/flotadamascofecha`,{params})
        }
}
