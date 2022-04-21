
export const getMaterials = async (page?: number,
                                   pageSize?: number,
                                   text?: string,
                                   tags?: string[] | null,
                                   schoolArea?: string | null,
                                   teacherId?: number): Promise<any[]> => {
    return await fetch('/Material/GetByFilter',
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                page: page ?? 1,
                pageSize: pageSize ?? 6,
                text: text ?? null,
                tags: tags ?? null,
                schoolArea: schoolArea ?? null,
                teacherId: teacherId ?? null,
                dateTime: null
            })
        },)
        .then(response => response.json())
        .then(res => res)
        .catch(error => console.log(error));
}

export const getEvents = async (page?: number,
                                pageSize?: number,
                                text?: string,
                                tags?: string[] | null,
                                schoolArea?: string | null,
                                teacherId?: number): Promise<any[]> => {
    return await fetch('/Activity/GetByFilter',
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                page: 1,
                pageSize: 5,
                text: text ?? null,
                tags: null,
                schoolArea: null,
                teacherId: null,
                dateTime: null
            })
        },)
        .then(response => response.json())
        .then(result => result)
        .catch(error => console.log(error));
}

export const getTeachers = async (page?: number,
                           pageSize?: number,
                           text?: string,
                           tags?: string[] | null,
                           schoolArea?: string | null,
                           teacherId?: number): Promise<any[]> => {
    return await fetch('/User/GetByFilter',
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                page: 1,
                pageSize: 5,
                text: text ?? null,
                tags: null,
                schoolArea: null,
                teacherId: null,
                dateTime: null
            })
        },)
        .then(response => response.json())
        .then(result => result)
        .catch(error => console.log(error));
}

export const getCurrentUser = async () => {
    return await fetch('/User/GetCurrentUserProfile',
        {
            method: 'GET',
        })
        .then(response => response.json())
        .then(result => result)
        .catch(error => console.log(error));
}

export const getProfile = async (id: string) => {
    return await fetch(`/User/GetProfile?id=${id}`)
        .then(response => response.json())
        .then(result => result)
        .catch(error => console.log(error));
}

export const updateProfile = async (
    id: string,
    name: string,
    description: string,
    image: string,
) => {
    return await fetch('/User/UpdateProfile',
        {
            method: 'POST',
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            body: JSON.stringify({
                id: id,
                name: name,
                description: description,
                image: image,
            })
        })
        .then()
}

export const getMaterial = async (id: number) => {
    return await fetch(`/Material/GetById?id=${id}`,)
        .then(response => response.json())
        .then(result => result)
        .catch(error => console.log(error));
}



