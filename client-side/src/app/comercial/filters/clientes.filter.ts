import { PipeTransform, Pipe } from '@angular/core';
import { Cliente } from '../models/cliente';

@Pipe({
    name: 'clientesFilter'
})
export class ClientesFilter implements PipeTransform {
    transform(cliente: Cliente[], searchFilter: string): Cliente[] {
        if (!cliente || !searchFilter) {
            return cliente;
        }

        return cliente.filter(cliente =>
            cliente.nomeCompleto.toLowerCase().indexOf(searchFilter.toLowerCase()) !== -1 || 
            cliente.email.toLowerCase().indexOf(searchFilter.toLowerCase()) !== -1
            );
            
    }
}