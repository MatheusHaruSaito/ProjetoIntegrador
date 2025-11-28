
export interface UserProfileEditRequest{
    id: string
    name:string
    email:string
    description:string
    profileImgPath: string
    profileImg?: File| null

}