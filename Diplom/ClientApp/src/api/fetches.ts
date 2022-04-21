
export const getMaterials = async (page?: number,
                                   pageSize?: number,
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