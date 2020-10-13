export interface UsersResponse{
    success: boolean,
    errors: string[],
    users: User[],
    count: number
}

export interface User{
    id: string,
    email: string,
    fullName: string
}