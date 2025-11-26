export interface PostComment {
    id: string;
    text: string;
    vote: number;
    userId: string;
    userUsername: string;
    userProfileImgPath: string;
    createdAt: string;
    isActive: boolean;
}
