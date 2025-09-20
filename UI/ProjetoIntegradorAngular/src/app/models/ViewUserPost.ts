export interface ViewUserPost{
    id: string,
    title: string,
    description: string
    userId: string
    votes: number,
    createAt: Date,
    updateTime: Date,
    postImg?: string,
}