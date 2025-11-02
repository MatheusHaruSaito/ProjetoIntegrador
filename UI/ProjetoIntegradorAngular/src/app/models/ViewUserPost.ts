export interface ViewUserPost{
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
    ProfileImgPath: string,

}