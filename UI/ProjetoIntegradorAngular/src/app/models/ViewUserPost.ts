export interface ViewUserPost{
    id: string,
    title: string,
    description: string
    userId: string
    votes: number,
    createdAt: Date,
    updateTime: Date,
    postImgPath?: string,
}