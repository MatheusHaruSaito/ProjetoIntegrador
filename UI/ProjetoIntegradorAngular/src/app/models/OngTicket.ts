export interface OngTicket{
    id: string;
    reviwed: boolean;
    accpeted: boolean;
    description: string;
    name: string;
    email: string;
    cep: string;
    cnpj: string;
    expirationDate : Date;
}