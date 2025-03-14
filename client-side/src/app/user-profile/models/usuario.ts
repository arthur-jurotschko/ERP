export class Usuario {
    id: string;
    nome: string;
    CPF: string;
    email: string;
    senha: string;
    senhaConfirmacao: string;
}

export class ConfirmacaoEmail {
    email: string;   
}

export class ForgotPassword {
    email: string;   
}

export class ResetPassword {
    email: string;
    password: string;
    confirmPassword: string;
    code: string;
}

export class Signup {
    nome: string;
    cpf: string;
    email: string;
    senha: string;
    senhaConfirmacao: string;
}