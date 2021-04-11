using testRogueSharp.Interfaces;
using System.Collections.Generic;
using System.Linq;
using testRogueSharp.Core;
using System;

namespace testRogueSharp.Systems{

    public class SchedulingSystem{
        private int time;
        private readonly SortedDictionary<int,List<IScheduleable>> scheduleables;
        //dictionnaire de tous les éléments soumis à un schedule 

        public SchedulingSystem(){
            time=0;
            scheduleables = new SortedDictionary<int, List<IScheduleable>>();
        }

        //Ajoute un nouvel objet au schedule
        //Le place au current time + le temps de l'objet
        public void Add(IScheduleable scheduleable){
            int key = time + scheduleable.Time; // TRÈS IMPORTANT
            // La key de schedulable c'est la valeur du temps actuel + la vitesse de l'objet
            // Par exemple si on est à 10 en time et que l'objet a 14 de speed, il devra attendre qu'on soit
            // À 24 pour bouger, tous les autres vont bouger avant, Si c'ests au joueur de bouger par exemple et qu'il a 10 de speed
            // Il pourra bouger, time sera alors égal à 20 (car il avait 10 de speed, il a été ajouté lorsque time était à 10 donc sa valeur dans key est 20)
            // Et il pourra bouger encore une fois car le monstre a sa key à 24.
            if(!scheduleables.ContainsKey(key)){
                scheduleables.Add(key,new List<IScheduleable>());
            }
            scheduleables[key].Add(scheduleable);
        }

        //Remove un objet du schedule
        //Utilisé notamment à la mort d'un monstre avant qu'il agisse
        public void Remove(IScheduleable scheduleable){
            // On trouve la liste contenant l'objet scheduleable en question parmis toutes les listes de scheduleable qu'on a
            KeyValuePair<int, List<IScheduleable>> scheduleableListFound = new KeyValuePair<int, List<IScheduleable>>(-1,null);
            foreach(KeyValuePair<int, List<IScheduleable>> scheduleablesList in scheduleables){
                if(scheduleablesList.Value.Contains(scheduleable)){
                    scheduleableListFound = scheduleablesList;
                    break;
                }
            }

            if(scheduleableListFound.Value != null){ //l'objet existe
                scheduleableListFound.Value.Remove(scheduleable); // on le supprime
                if(scheduleableListFound.Value.Count <=0){ // S'il n'y a pas d'autres scheduleables à ce time key alors on supprime la key
                    scheduleables.Remove(scheduleableListFound.Key);
                }
            }
        }

        // Retourne le prochain objet à qui c'est le tour, avance le temps si c'est necessaire
        public IScheduleable Get(){
            KeyValuePair<int,List<IScheduleable>> firstScheduleableGroup = scheduleables.First();
            IScheduleable firstScheduleable = firstScheduleableGroup.Value.First(); // On recupere le premier élément qui doit agir
            Remove(firstScheduleable); // on le supprime 
            time=firstScheduleableGroup.Key; // on avance le temps au moment ou on a le premier scheduleable
            Console.WriteLine(this);
            return firstScheduleable;
        }

        public int GetTime(){
            return time;
        }

        // Remet le temps à 0 et clear le schedule
        public void Clear(){
            time = 0;
            scheduleables.Clear();
        }

        public override string ToString()
        {
            string ret = "time = " + time + " ; \n";
            foreach(var sch in scheduleables){
                ret+="Key : " + sch.Key  + "\n";
                foreach(Actor actor in sch.Value){
                    ret+=$"    actor {actor.Name} ; speed = {actor.Speed} \n";
                }
            }
            return ret;
        }
    }
}