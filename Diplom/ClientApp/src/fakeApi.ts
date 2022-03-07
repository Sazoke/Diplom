import image from './Icons/portrait.webp';
import image1 from './Icons/1.jpg';
import image2 from './Icons/81aKwYDGd4Y.jpg'
import image3 from './Icons/WDX7Vtm-OB0.jpg'


export const profileObject = {
    name: "Соловьев Иван Дмитриевич",
    description: "Я Ваня",
    avatar: image,
    blocksMaterials: [{
        contentName: "Материал 1",
        contentTypeOrDate: "Разновидность 1",
        contentImage: image1
    },
        {
            contentName: "Материал 2",
            contentTypeOrDate: "Разновидность 2",
            contentImage: image2
        },
        {
            contentName: "Материал 3",
            contentTypeOrDate: "Разновидность 3",
            contentImage: image3
        },
        {
            contentName: "Материал 4",
            contentTypeOrDate: "Разновидность 4",
        }
    ],
    blocksEvents: [{
        contentName: "Мероприятие 1",
        contentTypeOrDate: new Date(),
        contentImage: ""
    },
        {
            contentName: "Мероприятие 2",
            contentTypeOrDate: new Date(),
            contentImage: ""
        },
        {
            contentName: "Мероприятие 3",
            contentTypeOrDate: new Date(),
            contentImage: ""
        },
        {
            contentName: "Мероприятие 4",
            contentTypeOrDate: new Date(),
            contentImage: ''
        }
    ],
    photos: [image, image3, image1, image2, image, image3, image1, image2]
}

export interface Content {
    contentName: string,
    contentTypeOrDate: string | Date,
    contentImage?: string
}
