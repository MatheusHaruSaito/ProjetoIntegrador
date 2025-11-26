import { User } from "./user";
import { ViewUserPost } from "./ViewUserPost";
import { PaginationData } from "./PaginationData";
export interface SearchResult{
    user: User[]
    posts: ViewUserPost[]
    paginationData: PaginationData
}