export interface UpdateUser{
    id:string;
    name:string
    password:string
    email:string
    description:string
    profileImgPath?: string;
    profileImg?: File| null
}