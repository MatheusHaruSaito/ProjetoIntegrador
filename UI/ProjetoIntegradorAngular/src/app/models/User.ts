export interface User{
    id:string;
    role:string[]
    name:string
    password:string
    email:string
    description:string
    cep:string
    isActive:boolean
    creationDate: Date
    updateDate: Date
    profileImgPath: string
}