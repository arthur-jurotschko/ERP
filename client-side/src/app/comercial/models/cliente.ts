export class Cliente {
    id: string;
    nomeCompleto: string;
    cpf: string;
    rg: string;
    telResidencial: string;
    telCelular: string;
    email: string;
    redeSocial: string;
    data: Date;
    checkEndereco: boolean;
    masterUserId: string;
    endereco: Endereco;
    generoId: string;
    enderecoId: string;
}
export class Endereco {
    id: string;
    logradouro: string;
    numero: string;
    complemento: string;
    bairro: string;
    cep: string;
    cidade: string;
    estado: string;
    clienteId: string;
}
export class Genero {
    id: string;
    nome: string;
}
