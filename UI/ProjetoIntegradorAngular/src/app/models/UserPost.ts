import { PostComment } from "./PostComment"

export interface UserPost{
    id: string,
    title: string,
    description: string
    votes: number,
    createdAt: Date,
    updateTime: Date,
    postImgPath?: string,
    commentsCount: number,
    userId: string,
    userName: string,
    profileImgPath: string,
    comments: PostComment[]

}