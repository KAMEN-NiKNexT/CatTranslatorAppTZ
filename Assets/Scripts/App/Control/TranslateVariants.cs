using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateVariants : MonoBehaviour
{
    private Dictionary<string, List<string>> _catPhrases = new Dictionary<string, List<string>>
        {
            {"English", new List<string>{"Hello, human!", "I love napping in sunbeams.", "Feed me now!", "Let me outside, please.", "I caught a mouse for you.", "The birds outside are taunting me.", "I demand treats!", "Scratch behind my ears, human.", "Why is the red dot always out of reach?", "I'm the ruler of this household."}},
            {"Russian", new List<string>{"Привет, человек!", "Обожаю валяться на солнце.", "Корми меня сейчас!", "Отпусти меня на улицу, пожалуйста.", "Я поймал для тебя мышь.", "Птицы снаружи издеваются надо мной.", "Требую угощений!", "Почеши меня за ушком, человек.", "Почему красная точка всегда недосягаема?", "Я владыка этого дома."}},
            {"Spanish", new List<string>{"¡Hola, humano!", "Me encanta dormir al sol.", "Aliméntame ahora mismo.", "Déjame salir, por favor.", "Atrapé un ratón para ti.", "Los pájaros afuera se burlan de mí.", "Exijo golosinas.", "Rasca detrás de mis orejas, humano.", "¿Por qué siempre está fuera de alcance el punto rojo?", "Soy el dueño de esta casa."}},
            {"Portuguese", new List<string>{"Olá, humano!", "Eu adoro cochilar ao sol.", "Me alimente agora!", "Deixe-me sair, por favor.", "Eu peguei um rato para você.", "Os pássaros lá fora estão me provocando.", "Eu exijo petiscos.", "Cocegue atrás das minhas orelhas, humano.", "Por que o pontinho vermelho está sempre fora de alcance?", "Eu sou o governante desta casa."}}
        };
}