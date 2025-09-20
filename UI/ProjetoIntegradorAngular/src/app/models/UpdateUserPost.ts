export interface UpdateUserPost{
    id: string,
    title: string,
    description: string
    userId: string
    votes: number,
    createAt: Date,
    updateTime: Date,
    postImgPath?: string,
    postImg?: File| null

}