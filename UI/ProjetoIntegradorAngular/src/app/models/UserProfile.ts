import { ViewUserPost } from "./ViewUserPost"

export interface UserProfile{
    name:string
    email:string
    description:string
    userPosts: ViewUserPost[]
    cep:string
    isActive:boolean
    creationDate: Date
    updateDate: Date
    profileImgPath: string
}