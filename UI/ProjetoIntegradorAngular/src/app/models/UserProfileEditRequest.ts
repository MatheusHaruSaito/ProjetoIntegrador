
export interface UserProfileEditRequest{
    name:string
    email:string
    description:string
    profileImgPath: string
    profileImg?: File| null

}