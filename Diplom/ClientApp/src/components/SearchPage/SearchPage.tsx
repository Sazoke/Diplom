import React, {useState} from "react";
import {SearchElement} from "../Elements/SearchElement";
import './SearchPage.css';
import {Dropdown, Modal, MenuItem, ComboBox, MenuSeparator} from "@skbkontur/react-ui";

export const SearchPage = () => {

    const [filtersOpened, setFiltersOpened] = useState(false);

    let materials = [{name: 'Material1'}, {name: 'Material2'}];
    let events = [{name: 'Event1'}, {name: 'Event2'}];
    let teachers = [{name: 'teacher1'}, {name: 'teacher2'}];
    let tests = [{name: 'test1'}, {name: 'test2'}];
    let schoolAreas = [{value: 0, label: 'Русский язык'}, {value: 1, label: 'Математика'}, {value: 2, label: 'Литература'}]


    const [schoolArea, setSchoolArea] = useState<any>(null);

    const delay = (time: number) => (args: any) => new Promise<number[]>(resolve => setTimeout(resolve, time, args));

    const getItems = (query: string) =>
        Promise.resolve(
            schoolAreas.filter(x => x.label.toLowerCase().includes(query.toLowerCase()) || x.value.toString(10) === query)
                .map(({ label, ...rest }) => {
                    const start = label.toLowerCase().indexOf(query.toLowerCase());
                    const end = start + query.length;

                    return {
                        ...rest,
                        label,
                        highlightedLabel:
                            start >= 0 ? (
                                <span>
                {label.substring(0, start)}
                                    <strong
                                        style={{
                                            fontSize: '1.1em',
                                        }}
                                    >
                  {label.substring(start, end)}
                </strong>
                                    {label.substring(end)}
              </span>
                            ) : null,
                    };
                }),
        ).then(delay(500));

    const [error, setError] = React.useState(false);

    let handleValueChange = (value: number) => {
        setSchoolArea(value);
        setError(false);
    };

    let handleUnexpectedInput = () => {
        setSchoolArea(null);
        setError(true);
    };

    let handleFocus = () => setError(false);

    const renderItem = (item: any) => {
        if (item.highlightedLabel) {
            return item.highlightedLabel;
        }

        return item.label;
    };

    const renderModal = () => <Modal onClose={() => setFiltersOpened(false)}>
        <Modal.Header> Фильтры </Modal.Header>
        <Modal.Body>
            <ComboBox
                error={error}
                getItems={getItems}
                onValueChange={handleValueChange}
                onFocus={handleFocus}
                onUnexpectedInput={handleUnexpectedInput}
                placeholder={schoolArea?.label ?? "Выберите предмет"}
                value={schoolArea?.label}
                renderItem={renderItem}
            />
        </Modal.Body>
    </Modal>

    return <div className={'search-area'}>
        <div className={'input-area'}>
            <input id={'search-input'} className={'search-input'}/>
            <button className={'search-button'}> Поиск </button>
        </div>
        <button className={'filter-button'} onClick={() => setFiltersOpened(true)}>Фильтры</button>
        <div className={'group-area'}>
            <div className={'group-title'}>Материалы</div>
            {materials.map(e =>
                <SearchElement name={e.name}/>
            )}
        </div>
        <div className={'group-area'}>
            <div className={'group-title'}>Преподаватели</div>
            {teachers.map(e =>
                <SearchElement name={e.name}/>
            )}
        </div>
        <div className={'group-area'}>
            <div className={'group-title'}>Мероприятия</div>
            {events.map(e =>
                <SearchElement name={e.name}/>
            )}
        </div>
        <div className={'group-area'}>
            <div className={'group-title'}>Тесты</div>
            {tests.map(e =>
                <SearchElement name={e.name} isTest/>
            )}
        </div>
        {filtersOpened && renderModal()}
    </div>
}