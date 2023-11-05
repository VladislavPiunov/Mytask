﻿Feature: Board
  Background:
    Given кейклоак работает и настроен
    When пользователь зарегистрирован в приложении
  
  Scenario: Пользователь может создать/отредактировать/получить список категорий/удалить категории
    When пользователь создает канбан-доску с названием "SomeBoardName"
    Then доска успешно создана
    And название доски "SomeBoardName"
    When пользователь обновляет название доски на "SomeBoardNameUpdated"
    Then доска успешно обновлена
    And название доски "SomeBoardNameUpdated"
    When пользователь запрашивает список всех досок
    Then список досок получен c количеством элементов 1    
    When пользователь удаляет доску
    Then доска успешно удалена
    When пользователь запрашивает список всех досок
    Then список досок получен c количеством элементов 0