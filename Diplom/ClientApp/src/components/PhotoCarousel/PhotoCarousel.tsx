import React, {useState} from "react";
import './PhotoCarousel.css';

export const PhotoCarousel = (props: {pics: string[], user: string, userPic: string}) => {

    const [currentPhoto, setCurrentPhoto] = useState<string | undefined>(undefined);

    const getPics = (pics: string[]) => {
        return pics.reduce<JSX.Element[]>(function (res, current) {
                res.push(
                    <li onClick={() => setCurrentPhoto(current)}>
                        <img src={current} />
                    </li>
            )
            return res;
            },[])
    }

    let position = 0;
    const nextClick = () => {
        if (position !== -264) {
            position -= 88;
            document.getElementById('picList')!.style.marginLeft = position + 'vw';
        }
    }
    const prevClick = () => {
        if (position !== 0) {
            position += 88;
            document.getElementById('picList')!.style.marginLeft = position + 'vw';
        }
    }

    return(
        <div className='carousel'>
            <button className='arrow prev' onClick={prevClick}>⮜</button>
            <div className='carousel-container'>
                <ul id='picList'>
                    {getPics(props.pics)}
                </ul>
            </div>
            <button className="arrow next" onClick={nextClick}>⮞</button>
            {currentPhoto
                ? <div className='current-photo'>
                    <div className='photo-with-description'>
                        <div className='overlay' onClick={() => setCurrentPhoto(undefined)}>{}</div>
                        <img src={currentPhoto} />
                        <div className='photo-description'>
                            <a>{}</a>
                        </div>
                    </div>
                </div>
                : undefined}
        </div>

    )
}