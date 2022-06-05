import React, {useEffect, useState} from "react";
import {SearchElement} from "../Elements/SearchElement";
import {getAreas, getEvents, getMaterials, getTeachers, getTypes} from "../../api/fetches";
import {Button, ComboBox, Modal, Paging} from "@skbkontur/react-ui";
import {useLocation} from "react-router-dom";

export const ElementsList = (props: {elementType: string, searchText?: string, teacherId?: string}) => {

    const [elements, setElements] = useState<any[]>([]);
    const [page, setPage] = useState(1);
    const search = useLocation();



    useEffect(() => {
        switch (props.elementType) {
            case 'Мероприятия':
                getEvents(page, 5, props.searchText, props.teacherId).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
                break;
            case 'Материалы':
                getMaterials(page, 5, props.searchText, props.teacherId).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
                break;
            case 'Преподаватели':
                getTeachers(page, 5, props.searchText).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
                break;
        }
    },[search,page])
    useEffect(() => {
        setPage(1);
        getAreas(setAreas)
        getTypes(setTypes);
    }, [search]);

    const searchWithFilters = () => {
        getMaterials(page, 5, props.searchText, props.teacherId, schoolArea?.id, type?.id).then(result => result.length > 0 ? setElements([...result]) : setElements([]));
        setFiltersOpened(false);
    }

    const [filtersOpened, setFiltersOpened] = useState(false);
    const [areas, setAreas] = useState<any[]>([]);
    const [types, setTypes] = useState<any[]>([]);

    const [schoolArea, setSchoolArea] = useState<any>(null);
    const [type, setType] = useState<any>(null);

    const delay = (time: number) => (args: any) => new Promise<number[]>(resolve => setTimeout(resolve, time, args));

    const getItems = (items: any[], query: string) =>
        Promise.resolve(
            items.filter(x => x.name?.toLowerCase().includes(query.toLowerCase()))
                .map(({ name, ...rest }) => {
                    const start = name.toLowerCase().indexOf(query.toLowerCase());
                    const end = start + query.length;

                    return {
                        ...rest,
                        name,
                        highlightedLabel:
                            start >= 0 ? (
                                <span>
                {name.substring(0, start)}
                                    <strong
                                        style={{
                                            fontSize: '1.1em',
                                        }}
                                    >
                  {name.substring(start, end)}
                </strong>
                                    {name.substring(end)}
              </span>
                            ) : null,
                    };
                }),
        ).then(delay(500));

    const getTypeItems = (items: any[], query: string) =>
        Promise.resolve(
            items.filter(x => x.multipleTypeName?.toLowerCase().includes(query.toLowerCase()))
                .map(({ multipleTypeName, ...rest }) => {
                    const start = multipleTypeName.toLowerCase().indexOf(query.toLowerCase());
                    const end = start + query.length;

                    return {
                        ...rest,
                        multipleTypeName,
                        highlightedLabel:
                            start >= 0 ? (
                                <span>
                {multipleTypeName.substring(0, start)}
                                    <strong
                                        style={{
                                            fontSize: '1.1em',
                                        }}
                                    >
                  {multipleTypeName.substring(start, end)}
                </strong>
                                    {multipleTypeName.substring(end)}
              </span>
                            ) : null,
                    };
                }),
        ).then(delay(500));

    const [areaError, setAreaError] = React.useState(false);

    const handleAreaChange = (setter: (value: any) => void, value: number) => {
        setSchoolArea(value);
        setAreaError(false);
    };

    const handleUnexpectedAreaInput = (setter: (value: any) => void) => {
        setSchoolArea(null);
        setAreaError(true);
    };

    const handleAreaFocus = () => setAreaError(false);

    const [typeError, setTypeError] = React.useState(false);

    const handleTypeChange = (setter: (value: any) => void, value: number) => {
        setType(value);
        setTypeError(false);
    };

    const handleUnexpectedTypeInput = (setter: (value: any) => void) => {
        setType(null);
        setTypeError(true);
    };

    const handleTypeFocus = () => setAreaError(false);

    const renderItem = (item: any) => {
        if (item.highlightedLabel) {
            return item.highlightedLabel;
        }
        return item.name;
    };

    const renderModal = () => <Modal onClose={() => setFiltersOpened(false)} width={600}>
        <Modal.Header> Фильтры </Modal.Header>
        <Modal.Body>
            <div className='modal-inner-wrapper'>
                <span> Предмет: </span>
                <ComboBox
                        error={areaError}
                        getItems={(query) => getItems(areas, query)}
                        onValueChange={(value) => handleAreaChange(setSchoolArea, value)}
                        onFocus={handleAreaFocus}
                        onUnexpectedInput={() => handleUnexpectedAreaInput(setSchoolArea)}
                        placeholder={schoolArea?.name ?? "Выберите предмет"}
                        value={schoolArea}
                        renderValue={item => item.name}
                        renderItem={renderItem}
                />
                <Button use={'danger'} onClick={() => setSchoolArea(null)}>Сбросить</Button>
                <span> Тип материала: </span>
                <ComboBox
                    style={{right: 0}}
                    error={typeError}
                    getItems={(query) => getTypeItems(types, query)}
                    onValueChange={(value) => handleTypeChange(setType, value)}
                    onFocus={handleTypeFocus}
                    onUnexpectedInput={() => handleUnexpectedTypeInput(setType)}
                    placeholder={type?.multipleTypeName ?? "Выберите тип материала"}
                    value={type}
                    renderValue={item => item.multipleTypeName}
                    renderItem={renderItem}
                />
                <Button use={'danger'} onClick={() => setType(null)}>Сбросить</Button>
                <Button onClick={searchWithFilters}>Применить</Button>
            </div>
        </Modal.Body>
    </Modal>

    return <div>
        {filtersOpened && renderModal()}
        {props.elementType === 'Материалы' && <Button onClick={() => setFiltersOpened(true)}>Фильтры</Button>}
        {elements.length > 0 ? <div className={'group-area'}>
                <div className={'group-title'}>{props.elementType}</div>
                {elements.map((e: any) =>
                    <SearchElement name={e.name}
                                   id={e.id}
                                   teacherId={e.teacherId}
                                   img={e.image}
                                   element={props.elementType}
                                   date={props.elementType === 'Мероприятия' ? e.date : null}
                    />
                )}
                <div className='paging-area'>
                    <Button disabled={page === 1} onClick={() => setPage(page - 1)}>{'<<'}</Button>
                    <span>{page}</span>
                    <Button disabled={elements.length < 5} onClick={() => setPage(page + 1)}>{'>>'}</Button>
                </div>
            </div>
            : <div className={'group-area'}>
                <div className={'group-title'}>{props.elementType}</div>
                <div style={{backgroundColor: "white", width: 200, marginBottom: 2}}>Нет подходящих вариантов</div>
                <div className='paging-area'>
                    <Button disabled={page === 1} onClick={() => setPage(page - 1)}>{'<<'}</Button>
                    <span>{page}</span>
                    <Button disabled={elements.length < 5} onClick={() => setPage(page + 1)}>{'>>'}</Button>
                </div>
            </div>
    }
    </div>
}
